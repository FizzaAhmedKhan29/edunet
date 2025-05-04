import { HttpClient, HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [HttpClient],
})
export class AppComponent {
  thumbnail: any = null;
  url: any = null;
  constructor(private http: HttpClient, private sanitizer: DomSanitizer) {}

  onBasicUpload(event: any): void {
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.url = event.target.result;
    };
    reader.onerror = (event: any) => {
      console.log("File could not be read: " + event.target.error.code);
    };
    reader.readAsDataURL(event['files'][0]);

    console.log(event['files'][0]);
    let param = new FormData();
    param.append('inputImage', event['files'][0]);
    this.http
      .post('https://localhost:7149/enhance/enlighten', param, {'responseType': 'blob'})
      .subscribe({
        next: (next:any) => {
          let dataType = next.type;
          let binaryData = [];
          binaryData.push(next);
          //let downloadLink = document.createElement('a');
          let objectURL = window.URL.createObjectURL(
            new Blob(binaryData, { type: dataType })
          );;
          this.thumbnail = this.sanitizer.bypassSecurityTrustUrl(objectURL);
          //downloadLink.href = objectURL;
          //document.body.appendChild(downloadLink);
          //downloadLink.click();
          //downloadLink.remove();
        },
      });
  }

  clear(){
    this.url = null;
    this.thumbnail = null;
  }
}
