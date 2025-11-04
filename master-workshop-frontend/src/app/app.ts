import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgClass } from "@angular/common";
import { ProductManager } from './services/product-manager';


export interface Producto {
  id: number
  nombre: string
  descripcion: string
  precio: number
  cantidad: number
  categoria: string
}

export interface ProductoForm {
  nombre: string
  descripcion: string
  precio: string
  cantidad: string
  categoria: string
}
@Component({
  selector: 'app-root',
  imports: [NgClass],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  private productManager = inject(ProductManager)

  products = signal<Producto[]>([])
  categorias = ['Computadoras', 'Accesorios', 'Monitores', 'Audio', 'Almacenamiento', 'Redes', 'Otros'];
  showAddForm = signal(false)
  showEditDialog = signal(false)
  showDeleteWarning = signal(false)
  selectedProduct = signal<Producto | null>(null)

  formData = signal<ProductoForm>({
    nombre: '',
    descripcion: '',
    precio: '',
    cantidad: '',
    categoria: ''
  })

  ngOnInit(): void {
    this.getAllProducts()
  }

  getAllProducts() {
    this.productManager.getAllProducts().subscribe({
      next: values => {
        this.products.set(values)
      }
    })
  }

  handleInputChange(e: HTMLTextAreaElement | HTMLInputElement | HTMLSelectElement) {
    if (e) {
      this.formData.set({ ...this.formData(), [e.name]: e.value });
    }
  };

  handleAddProduct(e: Event) {
    e.preventDefault();
    const newProduct = {
      id: 0,
      ...this.formData(),
      precio: parseFloat(this.formData().precio),
      cantidad: parseInt(this.formData().cantidad)
    };
    this.productManager.addNewProduct(newProduct).subscribe({
      next: result => {
        if (!result) return
        this.formData.set({ nombre: '', descripcion: '', precio: '', cantidad: '', categoria: '' });
        this.showAddForm.set(false);
        this.getAllProducts()
      }
    })
  };

  handleEditClick(product: Producto) {
    this.selectedProduct.set(product);
    this.formData.set({
      nombre: product.nombre,
      descripcion: product.descripcion,
      precio: product.precio.toString(),
      cantidad: product.cantidad.toString(),
      categoria: product.categoria
    });
    this.showEditDialog.set(true);
  };

  handleEditProduct(e: Event) {
    e.preventDefault();
    this.products.set(this.products().map(p =>
      this.selectedProduct() != null && p.id === this.selectedProduct()?.id
        ? { ...p, ...this.formData(), precio: parseFloat(this.formData().precio), cantidad: parseInt(this.formData().cantidad) }
        : p
    ));

    const updatedProduct: Producto = {
      id: this.selectedProduct()?.id ?? 0,
      ...this.formData(),
      precio: parseFloat(this.formData().precio),
      cantidad: parseInt(this.formData().cantidad)
    };
    this.productManager.updateProduct(updatedProduct).subscribe({
      next: response => {
        if (!response) return
        this.showEditDialog.set(false);
        this.selectedProduct.set(null);
        this.formData.set({ nombre: '', descripcion: '', precio: '', cantidad: '', categoria: '' });
        this.getAllProducts()
      }
    })

  };

  handleDeleteClick(product: Producto) {
    this.selectedProduct.set(product);
    this.showDeleteWarning.set(true);
  };

  confirmDelete() {
    this.productManager.deleteProduct(this.selectedProduct()?.id ?? 0).subscribe({
      next: res => {
        if (!res) return
        this.showDeleteWarning.set(false);
        this.selectedProduct.set(null);
        this.getAllProducts()
      }
    })

  };
}
