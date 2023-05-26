import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Order } from './models/order';
import { OrderService } from './services/order.service';
import { environment } from 'environments/environment';
import { LookUp } from 'app/core/models/lookUp';
import { Customer } from 'app/core/components/admin/customer/models/customer';
import { StorageDto } from '../storage/models/storageDto';
import { StorageService } from '../storage/services/storage.service';

declare var jQuery: any;

@Component({
	selector: 'app-order',
	templateUrl: './order.component.html',
	styleUrls: ['./order.component.scss']
})
export class OrderComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','createdUserId','createdDate','lastUpdatedUserId','lastUpdatedDate','status','isDeleted','customerId','productId','amount','size', 'update','delete'];
	userLookups:LookUp[];
	orderList:Order[];
	order:Order=new Order();
	storageList: StorageDto[];
	
	orderAddForm: UntypedFormGroup;


	orderId:number;

	constructor(private orderService:OrderService,private storageService:StorageService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: UntypedFormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOrderList();
		this.getUserLookUps();
		this.getStorageDtoList();
		
    }

	ngOnInit() {

		this.createOrderAddForm();
	}
	
	getStorageDtoList() {
		this.storageService.getStorageDtoList().subscribe((data) => {
		  this.storageList = data;
		  this.dataSource = new MatTableDataSource(data);
		  this.configDataTable();
		});
	  }
	getUserLookUps() {
		this.lookupService.getUserLookUp().subscribe(
		  lookUps => {
			this.userLookups = lookUps;
		  },
		  error => {
			console.error('Error retrieving user lookups:', error);
		  }
		);
	}

	getOrderList() {
		this.orderService.getOrderList().subscribe(data => {
			this.orderList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.orderAddForm.valid) {
			this.order = Object.assign({}, this.orderAddForm.value)

			if (this.order.id == 0)
				this.addOrder();
			else
				this.updateOrder();
		}

	}

	addOrder(){

		this.orderService.addOrder(this.order).subscribe(data => {
			this.getOrderList();
			this.order = new Order();
			jQuery('#order').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orderAddForm);
			

		})

	}

	updateOrder(){

		this.orderService.updateOrder(this.order).subscribe(data => {

			var index=this.orderList.findIndex(x=>x.id==this.order.id);
			this.orderList[index]=this.order;
			this.dataSource = new MatTableDataSource(this.orderList);
            this.configDataTable();
			this.order = new Order();
			jQuery('#order').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orderAddForm);

		})

	}

	createOrderAddForm() {
		this.orderAddForm = this.formBuilder.group({		
			id : [0],
createdUserId : [0, Validators.required],
createdDate : [null, Validators.required],
lastUpdatedUserId : [0, Validators.required],
lastUpdatedDate : [null, Validators.required],
status : [false, Validators.required],
isDeleted : [false, Validators.required],
customerId : [0, Validators.required],
productId : [0, Validators.required],
amount : [0, Validators.required],
size : ["", Validators.required]
		})
	}

	deleteOrder(orderId:number){
		this.orderService.deleteOrder(orderId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.orderList=this.orderList.filter(x=> x.id!=orderId);
			this.dataSource = new MatTableDataSource(this.orderList);
			this.configDataTable();
		})
	}

	getOrderById(orderId:number){
		this.clearFormGroup(this.orderAddForm);
		this.orderService.getOrderById(orderId).subscribe(data=>{
			this.order=data;
			this.orderAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: UntypedFormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

  }
