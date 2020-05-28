import { HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AuthModule, OidcConfigService, OidcSecurityService, OpenIdConfiguration, ConfigResult } from 'angular-auth-oidc-client';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FreeDataComponent } from './free-data/free-data.component';
import { PremiumDataComponent } from './premium-data/premium-data.component';

const oidc_configuration = 'assets/auth.clientConfiguration.json';

export function loadConfig(oidcConfigService: OidcConfigService) {
  return () => oidcConfigService.load(oidc_configuration);
}

@NgModule({
  declarations: [
    AppComponent,
    FreeDataComponent,
    PremiumDataComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AuthModule.forRoot(),
  ],
  providers: [
    OidcConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: loadConfig,
      deps: [OidcConfigService],
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private oidcSecurityService: OidcSecurityService, private oidcConfigService: OidcConfigService) {
    this.oidcConfigService.onConfigurationLoaded.subscribe((configResult: ConfigResult) => {

      // Use the configResult to set the configurations

      const config: OpenIdConfiguration = {
        stsServer: 'https://localhost:44375',
        redirect_url: 'https://localhost:5201',
        client_id: 'ngClient',
        scope: 'openid profile',
        response_type: 'id_token token',
        silent_renew: true,
        silent_renew_url: 'https://localhost:5201/silent-renew.html',
        log_console_debug_active: true,
        // all other properties you want to set
      };

      this.oidcSecurityService.setupModule(config, configResult.authWellknownEndpoints);
    });
  }
}
