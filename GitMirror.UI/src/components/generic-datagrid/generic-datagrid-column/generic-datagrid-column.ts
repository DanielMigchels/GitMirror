import { Component, ElementRef } from '@angular/core';

@Component({
  selector: 'app-generic-datagrid-column',
  standalone: true,
  templateUrl: './generic-datagrid-column.html',
  styleUrl: './generic-datagrid-column.css',
})
export class GenericDatagridColumn {
  constructor(public elementRef: ElementRef) {}
}
