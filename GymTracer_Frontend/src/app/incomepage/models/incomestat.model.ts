export interface IncomeStatItem {
  soldAmount: number;
  ticket: {
    type: number;
    description: string;
    isStudent: boolean;
    price: number;
    tax_key: number;
    maxUsage: number;
  }
}