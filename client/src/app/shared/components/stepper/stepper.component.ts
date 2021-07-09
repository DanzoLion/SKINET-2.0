import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-stepper',
  templateUrl: './stepper.component.html',
  styleUrls: ['./stepper.component.scss'],
  providers: [{provide: CdkStepper, useExisting: StepperComponent}]
})

export class StepperComponent extends CdkStepper implements OnInit {
@Input() linearModeSelected: boolean;


  ngOnInit() {
    this.linear = this.linearModeSelected;
  }

  onClick(index: number){
    this.selectedIndex = index;                                                                     // this keeps track of which step we are on
    // console.log(this.selectedIndex);                                                         // removes step log record within the console
  }


}
