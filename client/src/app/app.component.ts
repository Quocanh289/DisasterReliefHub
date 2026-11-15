import { Component } from '@angular/core';
import { LeafletMapComponent } from './map/leaflet-map.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [LeafletMapComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {}