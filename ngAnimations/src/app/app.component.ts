import { transition, trigger, useAnimation } from '@angular/animations';
import { Component } from '@angular/core';
import { bounce, shakeX, tada } from 'ng-animate';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  animations: [
    trigger("shake", [transition(":increment", useAnimation(shakeX, { params: { timing: 2 } }))]),
    trigger("bounce", [transition(":increment", useAnimation(bounce, { params: { timing: 4 } }))]),
    trigger("tada", [transition(":increment", useAnimation(tada, { params: { timing: 3 } }))])
  ]
})
export class AppComponent {
  title = 'ngAnimations';
  ng_shake = 0;
  ng_bounce = 0;
  ng_tada = 0;

  css_turn = false;

  constructor() {
  }

  animations(boucle: boolean) {
    this.ng_shake++;

    setTimeout(() => {
      this.ng_bounce++;
    }, 2000);

    setTimeout(() => {
      this.ng_tada++;
    }, 6000);

    setTimeout(() => {
      if (boucle)
        this.animations(true);
    }, 9000);
  }

  turn() {
    this.css_turn = true;

    setTimeout(() => {
      this.css_turn = false;
    }, 1000);
  }
}
