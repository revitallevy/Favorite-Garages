import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Garage } from '../models/garage';

/**
 * GarageApiService
 *
 * Responsible for communicating with the backend API
 * for retrieving government garages and managing user favorites.
 *
 * This service acts as a thin HTTP abstraction layer and
 * contains no business logic.
 */
@Injectable({
  providedIn: 'root',
})
export class GarageApiService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiBaseUrl;

   /**
   * Retrieves garages from the backend (proxied government API).
   */
  getGarages(limit = 20): Observable<Garage[]> {
    return this.http.get<Garage[]>(`${this.baseUrl}/garages?limit=${limit}`);
  }

   /**
   * Retrieves the list of favorite garages stored in the database.
   */
  getFavorites(): Observable<Garage[]> {
    return this.http.get<Garage[]>(`${this.baseUrl}/favorites`);
  }

  /**
   * Adds selected garages to the favorites list.
   * Backend prevents duplicates.
   */
  addFavorites(garages: Garage[]): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/favorites`, garages);
  }

   /**
   * Removes a garage from the favorites list by its external ID.
   */
  removeFavorite(externalGarageId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/favorites/${externalGarageId}`);
  }
}
