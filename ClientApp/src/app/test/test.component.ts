import { Component, OnInit } from '@angular/core';
import { IProduct } from '../models/product';
import { IProductBrand } from '../models/productBrand';
import { IProductCategory } from '../models/productCategory';
import { IProductType } from '../models/productType';
import { ShopParams } from '../models/shopParams';
import { TestService } from './test.service';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.scss']
})
export class TestComponent implements OnInit {
  products: IProduct[];
  productsWithParams: IProduct[];
  boxes: any[];
  productBrands: IProductBrand[];
  productTypes: IProductType[];
  productCategories: IProductCategory[];

  shopParams = new ShopParams();

  constructor(private testService: TestService) { }

  ngOnInit(): void {
    this.getProductBrands();
    this.getProductTypes();
    this.getProductCategories();
  }

  getProducts(): void {
    this.testService.getProducts(this.shopParams).subscribe(response => {
      this.products = response;
    }, error => {
      console.log(error);
    })
  }

  getProductsWithParams(): void {
    this.testService.getProducts(this.shopParams).subscribe(response => {
      this.productsWithParams = response;
      this.shopParams = new ShopParams();
    }, error => {
      console.log(error);
    })
  }

  getBoxes(): void {

  }
  getProductBrands(): void {
    this.testService.getProductBrands().subscribe(response => {
      this.productBrands = response;
      console.log(`From get brands`);
    }, error => {
      console.log(error);
    })
  }
  getProductTypes(): void {
    this.testService.getProductTypes().subscribe(response => {
      this.productTypes = response;
      console.log(this.products);
    }, error => {
      console.log(error);
    })
  }

  getProductCategories(): void {
    this.testService.getProductCategories().subscribe(response => {
      this.productCategories = response;
      console.log(this.products);
    }, error => {
      console.log(error);
    })
  }

  onBrandSelected(brandId: number): void {
    this.shopParams.brandId = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number): void {
    this.shopParams.typeId = typeId;
    this.getProducts();
  }

  onCategorySelected(categoryId: number): void {
    this.shopParams.categoryId = categoryId;
    this.getProducts();
  }

}
