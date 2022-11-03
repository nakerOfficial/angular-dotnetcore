import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-rates',
  templateUrl: './rates.component.html',
  styleUrls: ['./rates.component.css']
})
export class RatesComponent implements OnInit {
  rateCurrent = null;
  rates: any[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getRates();
  }

  getRates() {
    this.http.get(environment.host + "/Home/GetRatesFromBank")
      .subscribe((result: any[]) => this.rates = result.map(a => {
        a.value = Number(a.value).toFixed(3);

        return a;
      }));
  }

  setRateCurrent(name: string) {
    this.rateCurrent = this.rates.find(a => a.name === name);
  }
}
