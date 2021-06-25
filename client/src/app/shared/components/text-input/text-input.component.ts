import { Component, ElementRef, Input, OnInit, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})

export class TextInputComponent implements OnInit, ControlValueAccessor {       // below we set-up our component to use ControlValueAccessor
  @ViewChild('input', {static: true}) input: ElementRef;
  @Input() type = 'text';
  @Input() label: string;

  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this;                                                                     // binds to our class and provides access to controlDir inside our component and template
   }

  ngOnInit()  {
    const control = this.controlDir.control;
    const validators = control.validator ? [control.validator] : [];                            // we will have access to the validators that have been set for control - checks for validators or sets to emtpy array | ie password etc
    const asyncValidators = control.asyncValidator ? [control.asyncValidator] : []   // same as above but for async validation where we message the API, API requests are async

    control.setValidators(validators);
    control.setAsyncValidators(asyncValidators);
    control.updateValueAndValidity();
  }

  onChange(event) {}
  onTouched() {}

  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '';
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
   this.onTouched = fn;
  }

}
