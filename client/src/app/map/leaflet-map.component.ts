import { AfterViewInit, Component, ElementRef, OnDestroy, ViewChild } from '@angular/core';
import * as L from 'leaflet';

@Component({
  selector: 'app-leaflet-map',
  standalone: true,
  template: '<div #mapElement class="map-canvas" role="application" aria-label="Interactive map of Vietnam"></div>',
  styles: [':host { display: block; } .map-canvas { width: 100%; height: min(62vh, 620px); min-height: 340px; background: #dcebe1; }']
})
export class LeafletMapComponent implements AfterViewInit, OnDestroy {
  @ViewChild('mapElement', { static: true }) private mapElement!: ElementRef<HTMLDivElement>;
  private map?: L.Map;

  ngAfterViewInit(): void {
    this.map = L.map(this.mapElement.nativeElement, { zoomControl: false }).setView([16.0, 108.0], 5);
    L.control.zoom({ position: 'bottomright' }).addTo(this.map);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 19,
      attribution: '&copy; OpenStreetMap contributors'
    }).addTo(this.map);
  }

  ngOnDestroy(): void {
    this.map?.remove();
  }
}