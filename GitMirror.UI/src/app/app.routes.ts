import { Routes } from '@angular/router';
import { BaseLayout } from './layout/base-layout/base-layout';
import { Overview } from './overview/overview';
import { Providers } from './providers/providers';
import { Mirrors } from './mirrors/mirrors';
import { Repositories } from './repositories/repositories';
import { Settings } from './settings/settings';
import { History } from './history/history';

export const routes: Routes = [{
  path: '',
  component: BaseLayout,
  children: [
    { path: '', component: Overview },
    { path: 'repositories', component: Repositories },
    { path: 'mirrors', component: Mirrors },
    { path: 'providers', component: Providers },
    { path: 'history', component: History },
    { path: 'settings', component: Settings },
  ]
},
{ path: '**', redirectTo: '', pathMatch: 'full' },
];