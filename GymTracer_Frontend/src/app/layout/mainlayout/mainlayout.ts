import { Component } from '@angular/core';
import { Header } from "../header/header";
import { Footer } from "../footer/footer";
import { RouterOutlet } from '@angular/router';
import { OnbehalfBanner } from "../onbehalf-banner/onbehalf-banner";

@Component({
  selector: 'app-mainlayout',
  imports: [Header, Footer, RouterOutlet, OnbehalfBanner],
  templateUrl: './mainlayout.html',
  styleUrl: './mainlayout.css',
})
export class Mainlayout {

}
