import {Component, ElementRef, Input, ViewChild, HostBinding} from '@angular/core';
import {v4} from "uuid";
import {NgForOf, NgIf} from "@angular/common";

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
export class ImageLoaderComponent {
  @Input() label = ""
  @Input() multiple: boolean = false
  @ViewChild('fileInput') fileInput: ElementRef | null = null
  imagePreviews: string[] = []

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement
    if (input.files) {
      this.processFiles(input.files)
    }
  }

  private processFiles(files: FileList) {
    for (let i = 0; i < files.length; i++) {
      const file = files[i]
      const reader = new FileReader()
      reader.onload = (e: any) => {
        if (this.multiple) {
          this.imagePreviews.push(e.target.result)
        } else {
          this.imagePreviews = [e.target.result]
        }
      }
      reader.readAsDataURL(file)
    }
  }

  deleteImage(img: string) {
    this.imagePreviews = this.imagePreviews.filter(image => image !== img)
  }
}
