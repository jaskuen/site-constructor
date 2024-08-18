import { Routes } from '@angular/router';
import {LoginComponent} from "./blocks/login/login.component";
import {RegisterComponent} from "./blocks/register/register.component";
import {MainComponent} from "./blocks/main/main.component";

export const routes: Routes = [
  {path: "login", component: LoginComponent},
  {path: "register", component: RegisterComponent},
  {path: "", component: MainComponent},
];
