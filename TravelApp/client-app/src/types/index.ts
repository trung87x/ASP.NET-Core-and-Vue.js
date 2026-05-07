export interface TourPackage {
  id: number;
  listId: number;
  name: string;
  price: number;
}

export interface TourList {
  id: number;
  city: string;
  country: string;
  about: string;
  packages: TourPackage[];
}
