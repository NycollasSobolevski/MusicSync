import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { MenuComponent } from './menu/menu.component';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { LoginComponent } from './login/login.component';
import { SubscribeComponent } from './subscribe/subscribe.component';
import { FormsModule } from '@angular/forms';
import { CallbackPageComponent } from './callback-page/callback-page.component';
import { SpotifyComponent } from './CardComponents/spotify/spotify.component';
import { DeezerComponent } from './CardComponents/deezer/deezer.component';
import { UserConfigPageComponent } from './user-config-page/user-config-page.component';
import { ConfigMenuComponent } from './userConfigComponents/config-menu/config-menu.component';
import { StreamersConfigComponent } from './userConfigComponents/streamers-config/streamers-config.component';

@NgModule({
  declarations: [
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
    StreamersConfigComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
