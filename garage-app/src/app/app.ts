import { Component, computed, effect, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Garage } from './models/garage';
import { GarageApiService } from './services/garage-api-service';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

/**
 * AppComponent
 *
 * Main container component responsible for:
 * - Loading garages from the backend
 * - Managing selection state
 * - Handling favorites logic
 * - Filtering displayed data (show favorites only)
 *
 * State management is implemented using Angular signals
 * to keep the component reactive and zoneless.
 */
export class App {
  garages = signal<Garage[]>([]);
  favorites = signal<Garage[]>([]);
  selected = signal<Set<string>>(new Set());
  showFavoritesOnly = signal(false);
  isSaving = signal(false);

  displayedGarages = computed(() => {
    if (!this.showFavoritesOnly()) {
      return this.garages();
    }

    const favoriteIds = new Set(
      this.favorites().map(f => f.externalGarageId)
    );

    return this.garages().filter(g =>
      favoriteIds.has(g.externalGarageId)
    );
  });

  isLoading = signal(false);
  error = signal<string | null>(null);

  constructor(private apiService: GarageApiService) {
    this.loadInitialData();

    effect(() => {
      if (this.showFavoritesOnly()) {
        this.selected.set(new Set());
      }
    });
  }

  loadInitialData() {
    this.loadGarages();
    this.loadFavorites();
  }

  loadGarages() {
    this.isLoading.set(true);

    this.apiService.getGarages().subscribe({
      next: data => {
        this.garages.set(data);
        this.isLoading.set(false);
      },
      error: () => {
        this.error.set('Failed to load garages');
        this.isLoading.set(false);
      }
    });
  }

  loadFavorites() {
    this.apiService.getFavorites().subscribe({
      next: data => this.favorites.set(data)
    });
  }

  toggleSelection(id: string) {
    const current = new Set(this.selected());
    current.has(id) ? current.delete(id) : current.add(id);
    this.selected.set(current);
  }

  isFavorite(id: string) {
    return this.favorites().some(f => f.externalGarageId === id);
  }

  addSelectedToFavorites() {
    const selectedGarages = this.garages()
      .filter(g => this.selected().has(g.externalGarageId));

    if (selectedGarages.length === 0) return;

    this.isSaving.set(true);

    this.apiService.addFavorites(selectedGarages).subscribe({
      next: () => {
        this.selected.set(new Set());
        this.loadFavorites();
        this.isSaving.set(false);
      },
      error: () => this.isSaving.set(false)
    });
  }

  removeFavorite(id: string) {
    this.apiService.removeFavorite(id).subscribe({
      next: () => this.loadFavorites()
    });
  }

}
