import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {                   // product is then used int <app-product-item [product]="item"></app-product-item>
  @Input() product: IProduct;                                                                 // @ is a property used to accept a property from a parent component, ie from IProduct

  constructor() { }

  ngOnInit(): void {
  }

}
