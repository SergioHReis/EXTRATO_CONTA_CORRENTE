import { Component, OnInit } from '@angular/core';
import { LançamentoService } from './lançamento.service';
import { Lançamento } from './lançamento.model';

@Component({
  selector: 'app-extrato',
  templateUrl: './extrato.component.html',
  styleUrls: ['./extrato.component.css']
})
export class ExtratoComponent implements OnInit {
  lancamentos: Lançamento[];
  dataInicial: Date;
  dataFinal: Date;

  constructor(private lançamentoService: LançamentoService) { }

  ngOnInit() {
    this.dataInicial = new Date();
    this.dataFinal = new Date();
    this.dataInicial.setDate(this.dataInicial.getDate() - 2);
    this.atualizarExtrato();
  }

  atualizarExtrato() {
    this.lançamentoService.getLançamentos(this.dataInicial, this.dataFinal)
      .subscribe(
        (lançamentos) => {
          this.lancamentos = lançamentos;
        },
        (error) => {
          console.error('Erro ao buscar lançamentos:', error);
        }
      );
  }

  adicionarLancamento() {
    // Implemente a lógica para adicionar um novo lançamento
  }

  editarLancamento(lancamento: Lançamento) {
    // Implemente a lógica para editar um lançamento existente
  }

  cancelarLancamento(lancamento: Lançamento) {
    // Implemente a lógica para cancelar um lançamento existente
  }

  calcularTotal() {
    return this.lancamentos.reduce((total, lancamento) => total + lancamento.Valor, 0);
  }
}
