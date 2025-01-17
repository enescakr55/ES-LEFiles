import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'lib-corelib',
    template: `
    <p>
      corelib works!
    </p>
  `,
    styles: [],
    standalone: false
})
export class CorelibComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
