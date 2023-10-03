import { Component, OnInit, OnDestroy } from '@angular/core';  // Added OnDestroy
import { ConfigService } from './config.service';
import { Config } from './config';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-config',
  templateUrl: './config.component.html',
  styleUrls: ['./config.component.css']
})
export class ConfigComponent implements OnInit {  // Implemented OnDestroy

  config: Config = {} as Config;
  headers: string[] = [];

  constructor(private configService: ConfigService) { }

  ngOnInit(): void {
    this.showConfig();
  }

  showConfig() {
    this.configService.getConfig()
      .subscribe((data: Config) => this.config = { ...data });
  }

  showConfigResponse() {
    this.configService.getConfigResponse()
      .subscribe(resp => {
        // display its headers
        const keys = resp.headers.keys();
        this.headers = keys.map(key =>
          `${key}: ${resp.headers.get(key)}`);
          
    this.config = resp.body as Config;
    });
  }
}
