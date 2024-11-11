export interface ExchangeRate {
    currency: string;
    code: string;
    mid: number;
}

export interface ExchangeRatesResponse {
    table: string;
    no: string;
    effectiveDate: string;
    rates: ExchangeRate[];
}

export interface SingleRateResponse {
    currency: string;
    code: string;
    rates: { mid: number; effectiveDate: string }[];
}