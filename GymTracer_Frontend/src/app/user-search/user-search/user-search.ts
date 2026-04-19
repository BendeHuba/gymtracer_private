import { Component, inject, OnInit } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ThemeService } from '../../services/theme.service';
import { UserSearchResult } from '../models/user-search.model';
import { UserRole } from '../../models/user.role.model';
import { UserModel } from '../../models/user.model';
import { UserSearchService } from '../user.search.service';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [NgClass, FormsModule],
  templateUrl: './user-search.html',
  styleUrl: './user-search.css',
  host: { class: 'flex-1 flex flex-col w-full' }
})
export class UserSearch implements OnInit {
  auth = inject(AuthService);
  theme = inject(ThemeService);
  userSearchService = inject(UserSearchService);
  router = inject(Router);
  route = inject(ActivatedRoute);

  UserRole = UserRole;

  isLoading = false;
  hasSearched = false;
  errorMessage: string | null = null;
  users: UserSearchResult[] = [];

  searchName = '';
  searchEmail = '';
  searchGuid = '';

  secretSearchGuid?: string;
  secretSearchingMessage: string | null = null;


  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      if (params['guid']) {
        this.secretSearchGuid = params['guid'];
        
        this.onSecretSearch();
      }
    });
  }

  onSecretSearch(){
    this.isLoading = true;
    this.errorMessage = null;
    this.hasSearched = true;
    this.secretSearchingMessage = "Qr kód alapú keresés folyamatban...";
    this.userSearchService.searchUsers(
      undefined,
      undefined,
      this.secretSearchGuid
    ).subscribe({
      next: (res) => {
        this.isLoading = false;
        this.secretSearchGuid = undefined;
        this.secretSearchingMessage = null;
        if(res.length == 0){
          this.errorMessage = 'A beolvasott qr kód nem érvényes!';
        }
        else{
          this.viewAs(res[0]);
        }
      },
      error: (err) => {
        this.isLoading = false;
        this.secretSearchGuid = undefined;
        this.secretSearchingMessage = null;
        this.errorMessage = 'A beolvasott qr kód nem érvényes!';
      }
    });
  }

  onSearch() {
    this.isLoading = true;
    this.errorMessage = null;
    this.hasSearched = true;
    this.userSearchService.searchUsers(
      this.searchName || undefined,
      this.searchEmail || undefined,
      this.searchGuid || undefined
    ).subscribe({
      next: (res) => {
        this.users = res;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = err.error || 'Nem sikerült keresni a felhasználók között.';
        this.isLoading = false;
      }
    });
  }

  clearSearch() {
    this.searchName = '';
    this.searchEmail = '';
    this.users = [];
  }

  viewAs(user: UserSearchResult) {
    const userModel: UserModel = {
      id: user.id,
      name: user.name,
      email: user.email,
      role: user.role
    };
    this.auth.setPretendedUser(userModel);
    this.router.navigate(['/profile']);
  }

  getRoleLabel(role: UserRole) {
    switch(role) {
      case UserRole.admin: return 'Admin';
      case UserRole.staff: return 'Személyzet';
      case UserRole.trainer: return 'Edző';
      case UserRole.customer: return 'Vendég';
      default: return '?';
    }
  }

  getRoleBadgeClass(role: UserRole) {
    switch(role) {
      case UserRole.admin: return 'bg-purple-100 text-purple-800';
      case UserRole.staff: return 'bg-yellow-100 text-yellow-800';
      case UserRole.trainer: return 'bg-blue-100 text-blue-800';
      case UserRole.customer: return 'bg-green-100 text-green-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  }
}
