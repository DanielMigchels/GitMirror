import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { GenericBanner } from '../../components/generic-banner/generic-banner';

@Component({
  selector: 'app-history',
  imports: [RouterLink, GenericBanner],
  templateUrl: './history.html',
  styleUrl: './history.css',
})
export class History {

}
