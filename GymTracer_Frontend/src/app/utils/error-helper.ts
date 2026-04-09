export function formatErrors(err: any): string {
  if (err.error?.errors && typeof err.error.errors === 'object') {
    return Object.values(err.error.errors).join(' ');
  }
  return err.error?.error || err.error || 'Ismeretlen hiba történt.';
}
