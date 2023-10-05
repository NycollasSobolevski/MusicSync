import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './Components/nav/nav.component';
import { MenuComponent } from './Components/menu/menu.component';
import { HomePageComponent } from './Pages/home-page/home-page.component';
import { LoginPageComponent } from './Pages/login-page/login-page.component';
import { LoginComponent } from './Components/login/login.component';
import { SubscribeComponent } from './Components/subscribe/subscribe.component';
import { FormsModule } from '@angular/forms';
import { CallbackPageComponent } from './Pages/callback-page/callback-page.component';
import { SpotifyComponent } from './Components/CardComponents/spotify/spotify.component';
import { DeezerComponent } from './Components/CardComponents/deezer/deezer.component';
import { UserConfigPageComponent } from './Pages/user-config-page/user-config-page.component';
import { ConfigMenuComponent } from './Components/userConfigComponents/config-menu/config-menu.component';
import { StreamersConfigComponent } from './Components/userConfigComponents/streamers-config/streamers-config.component';
import { MusicPageComponent } from './Pages/music-page/music-page.component';
import { PlaylistPageComponent } from './Pages/playlist-page/playlist-page.component';
import { LoaderComponent } from './Components/loader/loader.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoaderInterceptor } from './interceptors/loader.interceptor';
import { VerifyEmailComponent } from './Components/verify-email/verify-email.component';
import { AlertComponent } from './Components/alert/alert.component';


const COMPONENTS = [
  AppComponent,
  NavComponent,
  MenuComponent,
  HomePageComponent,
  LoginPageComponent,
  LoginComponent,
  SubscribeComponent,
  CallbackPageComponent,
  SpotifyComponent,
  DeezerComponent,
  UserConfigPageComponent,
  ConfigMenuComponent,
  StreamersConfigComponent,
  MusicPageComponent,
  PlaylistPageComponent,
  LoaderComponent
]
@NgModule({
  declarations: [COMPONENTS, VerifyEmailComponent, AlertComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  exports: [COMPONENTS],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
