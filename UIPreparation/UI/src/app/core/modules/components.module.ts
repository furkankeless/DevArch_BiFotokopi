import { NgModule } from '@angular/core';
import { FooterComponent } from '../components/app/footer/footer.component';
import { NavbarComponent } from '../components/app/navbar/navbar.component';
import { SidebarComponent } from '../components/app/sidebar/sidebar.component';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';


@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    TranslateModule,
  ],
  declarations: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent
  ],
  exports: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent
  ]
})
export class ComponentsModule { }
