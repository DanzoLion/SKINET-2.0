import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderDetailedComponent } from './order-detailed/order-detailed.component';
import { OrdersRoutingModule } from './orders-routing.module';
import { OrdersComponent } from './orders.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [OrdersComponent, OrderDetailedComponent],
  imports: [CommonModule, OrdersRoutingModule, SharedModule]
  
})
export class OrdersModule {}
