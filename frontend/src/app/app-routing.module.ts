import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { HomePageComponent } from './Pages/home-page/home-page.component';
import { LoginPageComponent } from './Pages/login-page/login-page.component';
import { CallbackPageComponent } from './Pages/callback-page/callback-page.component';
import { UserConfigPageComponent } from './Pages/user-config-page/user-config-page.component';
import { PlaylistPageComponent } from './Pages/playlist-page/playlist-page.component';
import { MusicPageComponent } from './Pages/music-page/music-page.component';
import { ForgetPasswordPageComponent } from './Pages/forget-password-page/forget-password-page.component';

const routes: Routes = [
  { path: "", title: "Music Sync | Home", component: HomePageComponent},
  { path: "Login", title: "Music Sync | Login", component: LoginPageComponent},
  { path: "Callback", title:"Music Sync | Callback", component: CallbackPageComponent},
  { path: "settings", title: "Music Sync | Settings", component: UserConfigPageComponent},
  { path: "playlist", title: "Music Sync | Playlist", component: PlaylistPageComponent},
  { path: "music", title: "Music Sync | Music", component: MusicPageComponent},
  { path: "forgetPassword", title: "Music Sync | Forget Password", component: ForgetPasswordPageComponent},
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    HttpClientModule,
    FormsModule
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
