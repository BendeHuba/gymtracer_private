import { Component, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ScannerTrackerService } from './services/scanner.tracker.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit, OnDestroy {
  protected readonly title = signal('GymTracer_Frontend');

  scannerService = inject(ScannerTrackerService);

  ngOnInit() {
    this.scannerService.startTracking();
  }

  ngOnDestroy() {
    this.scannerService.stopTracking();
  }
}
