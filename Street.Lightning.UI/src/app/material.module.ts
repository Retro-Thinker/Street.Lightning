import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatGridListModule } from "@angular/material/grid-list";
import { MatInputModule } from "@angular/material/input";
import { MatSelectModule } from "@angular/material/select";
import { MatTableModule } from "@angular/material/table";
import { MatToolbarModule } from "@angular/material/toolbar";
import { RouterLink, RouterOutlet } from "@angular/router";


@NgModule({
   imports: [RouterOutlet, RouterLink, MatGridListModule, MatToolbarModule, HttpClientModule, MatButtonModule, MatTableModule, MatFormFieldModule, MatInputModule, MatSelectModule],
   exports: [RouterOutlet, RouterLink, MatGridListModule, MatToolbarModule, HttpClientModule, MatButtonModule, MatTableModule, MatFormFieldModule, MatInputModule, MatSelectModule]
})
export default class MaterialModule {}