import { Component, ElementRef } from '@angular/core';

@Component({
  selector: 'app-generic-datagrid-cell',
  standalone: true,
  templateUrl: './generic-datagrid-cell.html',
  styleUrl: './generic-datagrid-cell.css',
})
export class GenericDatagridCell {
  constructor(public elementRef: ElementRef) {}
}
