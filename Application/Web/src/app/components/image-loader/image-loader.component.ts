import {Component, ElementRef, Input, ViewChild, HostBinding, EventEmitter, Output, AfterViewInit} from '@angular/core';
import {v4} from "uuid";
import {NgForOf, NgIf} from "@angular/common";
import {Image} from "../../../types";

@Component({
  selector: 'app-image-loader',
  standalone: true,
  imports: [
    NgForOf,
    NgIf
  ],
  templateUrl: './image-loader.component.html',
  styleUrl: './image-loader.component.scss'
})
export class ImageLoaderComponent implements AfterViewInit {
  @Input() label = ""
  @Input() multiple: boolean = false
  @Input() type: string = "";
  @Input() imagePreviews: Image[] = []
  @Output() imagePreviewsChange: EventEmitter<Image[]> = new EventEmitter();
  @ViewChild('fileInput') fileInput: ElementRef | null = null
  id: string  = v4();

  setWrapperHeight(newFilesCount: number) {
    let wrapper = document.getElementById(this.id) as HTMLDivElement;
    let strings = Math.floor((this.imagePreviews.length + newFilesCount) / 4 ) + 1;
    wrapper.style.height = 160 * strings + (16 * strings - 1) + 'px';
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement
    if (input.files) {
      this.processFiles(input.files)
      this.setWrapperHeight(input.files.length)
    }
  }

  processFiles(files: FileList) {
    const userId = localStorage.getItem("userId")!;
    for (let i = 0; i < files.length; i++) {
      const randNum = Math.round(Math.random() * 100000 + 1)
      const file = files[i]
      const reader = new FileReader()
      reader.onload = (e: any) => {
        const result: Image = {
          type: this.type,
          imageFileBase64String: e.target.result,
        }
        if (this.multiple) {
          this.imagePreviews = [...this.imagePreviews, result]
        } else {
          this.imagePreviews = [result]
        }
        this.imagePreviewsChange.emit(this.imagePreviews)
      }
      reader.readAsDataURL(file)
    }
  }

  deleteImage(img: Image) {
    this.imagePreviews = this.imagePreviews.filter(image => image.imageFileBase64String !== img.imageFileBase64String)
    this.setWrapperHeight(0)
    this.imagePreviewsChange.emit(this.imagePreviews)
  }

  ngAfterViewInit() {
    this.setWrapperHeight(0)
  }
}
