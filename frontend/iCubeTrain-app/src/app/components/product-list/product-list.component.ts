import { Component, OnInit, inject } from '@angular/core';
import { IProduct } from '../../interfaces/product'
import { ProductService } from '../../services/product.service';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [MatTableModule, MatButtonModule],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent{
  productList:IProduct[] = [];
  productService=inject(ProductService);
  displayedColumns: string[] = ['id', 'name', 'price'];
  ngOnInit(){
    // CRUD operations
    this.productService.getProducts().subscribe((result: IProduct[]) => {
      this.productList = result;
      console.log(this.productList);
    });

  }

  goBack(){
    window.history.back();
  }
}
