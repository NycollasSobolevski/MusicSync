import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { FormsModule } from '@angular/forms';
import { CallbackPageComponent } from './callback-page/callback-page.component';
import { UserConfigPageComponent } from './user-config-page/user-config-page.component';
import { PlaylistPageComponent } from './CardComponents/playlist-page/playlist-page.component';

const routes: Routes = [
  { path: "", title: "Music Sync | Home", component: HomePageComponent},
  { path: "Login", title: "Music Sync | Login", component: LoginPageComponent},
  { path: "spotifyCallback", title:"Music Sync | Callback", component: CallbackPageComponent},
  { path: "settings", title: "Music Sync | Settings", component: UserConfigPageComponent},
  { path: "playlist", title: "Music Sync | Playlist", component: PlaylistPageComponent},
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
