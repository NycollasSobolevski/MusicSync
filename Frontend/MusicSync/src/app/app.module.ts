import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SpotifyCardComponent } from './spotify-card/spotify-card.component';
import { NavComponent } from './nav/nav.component';
import { MenuComponent } from './menu/menu.component';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { LoginComponent } from './login/login.component';
import { SubscribeComponent } from './subscribe/subscribe.component';
import { FormsModule } from '@angular/forms';
import { CallbackPageComponent } from './callback-page/callback-page.component';
import { StreamerCardComponent } from './streamer-card/streamer-card.component';
import { SpotifyComponent } from './CardComponents/spotify/spotify.component';

@NgModule({
  declarations: [
    AppComponent,
    SpotifyCardComponent,
    NavComponent,
    MenuComponent,
    HomePageComponent,
    LoginPageComponent,
    LoginComponent,
    SubscribeComponent,
    CallbackPageComponent,
    StreamerCardComponent,
    SpotifyComponent
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
