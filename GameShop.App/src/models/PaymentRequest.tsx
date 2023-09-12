export interface PaymentRequest {
  email: string;
  fullName: string;
  total: number;
  cardNumber: string;
  expirationYear: string;
  expirationMonth: string;
  cvc: string;
}
