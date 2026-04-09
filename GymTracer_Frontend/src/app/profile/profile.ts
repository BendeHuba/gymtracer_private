import { Component, inject, OnInit } from '@angular/core';
import { NgClass, SlicePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { ThemeService } from '../services/theme.service';
import { UserService } from '../services/user.service';
import { UserRole } from '../models/user.role.model';
import { formatErrors } from '../utils/error-helper';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [NgClass, FormsModule, SlicePipe],
  templateUrl: './profile.html',
  host: { class: 'flex-1 flex flex-col w-full' }
})
export class ProfilePage implements OnInit {
  auth = inject(AuthService);
  theme = inject(ThemeService);
  userService = inject(UserService);

  UserRole = UserRole;

  isLoading = true;
  isSaving = false;
  editMode = false;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  profile: { name: string; email: string; birthDate: string | null; creationDate: string; cards: number[] } | null = null;

  editData = { name: '', email: '', birthDate: '' };

  ngOnInit() {
    const userId = this.auth.actingUser?.id;
    if (!userId) return;
    this.loadProfile(userId);
  }

  loadProfile(id: number) {
    this.isLoading = true;
    this.errorMessage = null;
    this.userService.getProfile(id).subscribe({
      next: (res) => {
        this.profile = res.user;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = formatErrors(err);
        this.isLoading = false;
      }
    });
  }

  startEdit() {
    if (!this.profile) return;
    this.editData = {
      name: this.profile.name,
      email: this.profile.email,
      birthDate: this.profile.birthDate ? this.profile.birthDate.substring(0, 10) : ''
    };
    this.editMode = true;
    this.successMessage = null;
    this.errorMessage = null;
  }

  cancelEdit() {
    this.editMode = false;
    this.errorMessage = null;
  }

  saveProfile() {
    const userId = this.auth.actingUser?.id;
    if (!userId) return;
    this.isSaving = true;
    this.errorMessage = null;
    this.userService.updateProfile(userId, {
      name: this.editData.name,
      email: this.editData.email,
      birthDate: this.editData.birthDate || null
    }).subscribe({
      next: (res) => {
        this.profile = res.user;
        this.editMode = false;
        this.isSaving = false;
        this.successMessage = 'A profil sikeresen mentve!';
      },
      error: (err) => {
        this.errorMessage = formatErrors(err);
        this.isSaving = false;
      }
    });
  }

  getRoleLabel(role: UserRole | undefined): string {
    switch(role) {
      case UserRole.admin: return 'Adminisztrátor';
      case UserRole.staff: return 'Személyzet';
      case UserRole.trainer: return 'Edző';
      case UserRole.customer: return 'Vendég';
      default: return 'Ismeretlen';
    }
  }

  getRoleBadgeClass(role: UserRole | undefined): string {
    switch(role) {
      case UserRole.admin: return 'bg-purple-100 text-purple-800';
      case UserRole.staff: return 'bg-yellow-100 text-yellow-800';
      case UserRole.trainer: return 'bg-blue-100 text-blue-800';
      case UserRole.customer: return 'bg-green-100 text-green-800';
      default: return 'bg-gray-100 text-gray-800';
    }
  }
}
