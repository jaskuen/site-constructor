import { Routes } from '@angular/router';
import {MainComponent} from "./blocks/main/main.component";
import {LoginComponent} from "./blocks/auth/login/login.component";
import {RegisterComponent} from "./blocks/auth/register/register.component";

export const routes: Routes = [
  {path: "login", component: LoginComponent},
  {path: "register", component: RegisterComponent},
  {path: "", component: MainComponent},
];
