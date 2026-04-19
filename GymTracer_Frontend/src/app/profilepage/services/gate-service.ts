import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Usage_Gate } from '../userprofilemodels/gate.enum.model';
import { GateMessageDto } from '../userprofilemodels/gate.model';

@Injectable({
  providedIn: 'root',
})
export class GateService {
  http = inject(HttpClient);

  enterGate(GateId: Usage_Gate, cardCode: string, force: boolean, enter_main: boolean){
    const params = new HttpParams().set("force", force);
    return this.http.post<GateMessageDto>(`${environment.apiUrl}/Gate/${GateId}/card/${cardCode}${ enter_main ? '/enter-main' : ''}`,{},{params: params});
  }
}
