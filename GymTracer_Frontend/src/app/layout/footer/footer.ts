import { NgClass } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ThemeService } from '../../services/theme.service';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-footer',
  imports: [NgClass, RouterLink],
  templateUrl: './footer.html',
  styleUrl: './footer.css',
})
export class Footer {
  theme = inject(ThemeService);
}
