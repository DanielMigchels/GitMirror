import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NgIconComponent } from '@ng-icons/core';
import { GenericBanner } from '../../components/generic-banner/generic-banner';

@Component({
  selector: 'app-mirrors',
  imports: [RouterLink, GenericBanner],
  templateUrl: './mirrors.html',
  styleUrl: './mirrors.css',
})
export class Mirrors {

}
