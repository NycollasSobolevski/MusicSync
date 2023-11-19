import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './Components/nav/nav.component';
import { MenuComponent } from './Components/menu/menu.component';
import { HomePageComponent } from './Pages/home-page/home-page.component';
import { LoginPageComponent } from './Pages/login-page/login-page.component';
import { LoginComponent } from './Components/loginComponents/login/login.component';
import { SubscribeComponent } from './Components/loginComponents/subscribe/subscribe.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CallbackPageComponent } from './Pages/callback-page/callback-page.component';
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
import { CreatePlaylistComponent } from './Components/CardComponents/create-playlist/create-playlist.component';
import { MainCardComponent } from './Components/PlaylistTransferComponents/main-card/main-card.component';
import { SelectStreamerComponent } from './Components/PlaylistTransferComponents/select-streamer/select-streamer.component';
import { SelectPlaylistComponent } from './Components/PlaylistTransferComponents/select-playlist/select-playlist.component';
import { PlaylistSettingsComponent } from './Components/PlaylistTransferComponents/playlist-settings/playlist-settings.component';
import { AccountConfigComponent } from './Components/userConfigComponents/account-config/account-config.component';
import { ForgetPasswordPageComponent } from './Pages/forget-password-page/forget-password-page.component';
import { SendIdentifyComponent } from './Components/ForgetPassword/send-identify/send-identify.component';
import { SendTokenComponent } from './Components/ForgetPassword/send-token/send-token.component';
import { SendPasswordComponent } from './Components/ForgetPassword/send-password/send-password.component';
import { TransferTracksComponent } from './Components/PlaylistTransferComponents/transfer-tracks/transfer-tracks.component';
import { MenuCardComponent } from './Components/menu-card/menu-card.component';
import { PlaylistCardComponent } from './Components/CardComponents/playlist-card/playlist-card.component';


const COMPONENTS = [
  AppComponent,
  NavComponent,
  MenuComponent,
  HomePageComponent,
  LoginPageComponent,
  LoginComponent,
  SubscribeComponent,
  CallbackPageComponent,
  UserConfigPageComponent,
  ConfigMenuComponent,
  StreamersConfigComponent,
  MusicPageComponent,
  PlaylistPageComponent,
  LoaderComponent,
  AccountConfigComponent,
  VerifyEmailComponent, 
  AlertComponent, 
  CreatePlaylistComponent, 
  MainCardComponent, 
  SelectStreamerComponent, 
  SelectPlaylistComponent, 
  PlaylistSettingsComponent, 
  ForgetPasswordPageComponent,
  SendIdentifyComponent, 
  SendTokenComponent, 
  SendPasswordComponent, 
  TransferTracksComponent, 
  MenuCardComponent,
  PlaylistCardComponent,
]
@NgModule({
  declarations: [COMPONENTS, ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule
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
