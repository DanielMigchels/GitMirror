import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { provideIcons } from '@ng-icons/core';
import { heroSun, heroMoon, heroRectangleGroup, heroArrowPath, heroCog, heroCircleStack, heroClipboardDocumentCheck, heroBars3, heroXMark, heroFolderOpen, heroExclamationCircle, heroPlus, heroTrash, heroPencil, heroCodeBracket } from '@ng-icons/heroicons/outline';
import { simpleGithub, simpleGitlab, simpleBitbucket } from '@ng-icons/simple-icons';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    provideIcons({ heroSun, heroMoon, heroRectangleGroup, heroArrowPath, heroCog, heroCircleStack, heroClipboardDocumentCheck, heroBars3, heroXMark, heroFolderOpen, heroExclamationCircle, heroPlus, heroTrash, heroPencil, heroCodeBracket, simpleGithub, simpleGitlab, simpleBitbucket }),
  ]
};
