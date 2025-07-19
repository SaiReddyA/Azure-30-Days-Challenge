import {
  ApplicationConfig,
  importProvidersFrom,
  provideZoneChangeDetection,
  APP_INITIALIZER,
} from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import {
  HTTP_INTERCEPTORS,
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';

import {
  IPublicClientApplication,
  PublicClientApplication,
  InteractionType,
  BrowserCacheLocation,
  LogLevel,
} from '@azure/msal-browser';

import {
  MsalInterceptor,
  MSAL_INSTANCE,
  MsalInterceptorConfiguration,
  MsalGuardConfiguration,
  MSAL_GUARD_CONFIG,
  MSAL_INTERCEPTOR_CONFIG,
  MsalService,
  MsalGuard,
  MsalBroadcastService,
} from '@azure/msal-angular';
import { environment } from './environments/environment';

// Logger
export function loggerCallback(logLevel: LogLevel, message: string) {
  console.log(message);
}

// ✅ MSAL Instance (async)
export async function createMsalInstance(): Promise<IPublicClientApplication> {
  const instance = new PublicClientApplication({
    auth: {
      clientId: environment.msalConfig.auth.clientId,
      authority: environment.msalConfig.auth.authority,
      redirectUri: environment.msalConfig.auth.redirectUri,
      knownAuthorities: ['saireddydotnetfs.ciamlogin.com']
    },
    cache: {
      cacheLocation: BrowserCacheLocation.LocalStorage,
      storeAuthStateInCookie: false,
    },
    system: {
      loggerOptions: {
        loggerCallback,
        logLevel: LogLevel.Info,
        piiLoggingEnabled: false,
      },
    },
  });

  await instance.initialize();
  return instance;
}

// ✅ Store the initialized instance in a global variable
let msalInstance: IPublicClientApplication;

export function MSALInstanceFactory(): IPublicClientApplication {
  return msalInstance;
}

// ✅ Called before app bootstraps
export function initializeMsalApp(): () => Promise<void> {
  return async () => {
    msalInstance = await createMsalInstance();
  };
}

// ✅ MSAL Configs
export function MSALInterceptorConfigFactory(): MsalInterceptorConfiguration {
  const protectedResourceMap = new Map<string, Array<string>>();
  protectedResourceMap.set('https://graph.microsoft.com/v1.0/me', ['user.read']);

  return {
    interactionType: InteractionType.Redirect,
    protectedResourceMap,
  };
}

export function MSALGuardConfigFactory(): MsalGuardConfiguration {
  return {
    interactionType: InteractionType.Redirect,
    authRequest: {
      scopes: ['user.read'],
    },
    loginFailedRoute: '/login-failed',
  };
}

// ✅ Angular 18 app config
export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptorsFromDi()),

    // Initialize MSAL before bootstrapping app
    {
      provide: APP_INITIALIZER,
      useFactory: initializeMsalApp,
      multi: true,
    },

    // Provide MSAL instance after init
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory,
    },

    // Provide MSAL config
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MSALGuardConfigFactory,
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true,
    },

    MsalService,
    MsalGuard,
    MsalBroadcastService,
  ],
};
