import { Component, inject, OnInit } from '@angular/core';
import { NgClass } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ThemeService } from '../services/theme.service';
import { UserService } from '../services/user.service';
import { UserSearchResult } from '../models/user-search.model';
import { UserRole } from '../models/user.role.model';
import { UserModel } from '../models/user.model';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [NgClass, FormsModule],
  templateUrl: './users.html',
  host: { class: 'flex-1 flex flex-col w-full' }
})
export class UsersPage implements OnInit {
  auth = inject(AuthService);
  theme = inject(ThemeService);
  userService = inject(UserService);
  router = inject(Router);

  UserRole = UserRole;

  isLoading = false;
  hasSearched = false;
  errorMessage: string | null = null;
  users: UserSearchResult[] = [];
  searchName = '';
  searchEmail = '';

  ngOnInit() {
    this.onSearch();
  }

  onSearch() {
    this.isLoading = true;
    this.errorMessage = null;
    this.hasSearched = true;
    this.userService.searchUsers(
      this.searchName || undefined,
      this.searchEmail || undefined
    ).subscribe({
      next: (res) => {
        this.users = res;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = err.error?.error || 'Nem sikerült keresni a felhasználók között.';
        this.isLoading = false;
      }
    });
  }

  clearSearch() {
    this.searchName = '';
    this.searchEmail = '';
    this.onSearch();
  }

  viewAs(user: UserSearchResult) {
    const userModel: UserModel = {
      id: user.id,
      name: user.name,
      email: user.email,
      role: user.role
    };
    this.auth.pretendedUser = userModel;
    this.theme.isPretendMode = true;
    localStorage.setItem('pretended_user', JSON.stringify(userModel));
    this.router.navigate(['/trainings']);
  }

  getRoleLabel(role: UserRole): string {
    switch(role) {
      case UserRole.admin: return 'Admin';
      case UserRole.staff: return 'Személyzet';
      case UserRole.trainer: return 'Edző';
      case UserRole.customer: return 'Vendég';
      default: return '?';
    }
  }

  getRoleBadgeClass(role: UserRole): string {
    switch(role) {
      case UserRole.admin: return 'bg-purple-100 text-purple-800';
      case UserRole.staff: return 'bg-yellow-100 text-yellow-800';
      case UserRole.trainer: return 'bg-blue-100 text-blue-800';
      case UserRole.customer: return 'bg-green-100 text-green-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  }
}
