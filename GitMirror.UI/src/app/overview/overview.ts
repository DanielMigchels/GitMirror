import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NgIcon } from "@ng-icons/core";
import { GenericBanner } from '../../components/generic-banner/generic-banner';

@Component({
  selector: 'app-overview',
  imports: [RouterLink, GenericBanner],
  templateUrl: './overview.html',
  styleUrl: './overview.css',
})
export class Overview {

}
