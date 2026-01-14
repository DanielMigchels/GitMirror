import { Routes } from '@angular/router';
import { BaseLayout } from './layout/base-layout/base-layout';
import { Overview } from './overview/overview';
import { Mirrors } from './mirrors/mirrors';
import { Repositories } from './repositories/repositories';
import { Settings } from './settings/settings';
import { History } from './history/history';
import { Platforms } from './platforms/platforms';

export const routes: Routes = [{
  path: '',
  component: BaseLayout,
  children: [
    { path: '', component: Overview },
    { path: 'repositories', component: Repositories },
    { path: 'mirrors', component: Mirrors },
    { path: 'platforms', component: Platforms },
    { path: 'history', component: History },
    { path: 'settings', component: Settings },
  ]
},
{ path: '**', redirectTo: '', pathMatch: 'full' },
];