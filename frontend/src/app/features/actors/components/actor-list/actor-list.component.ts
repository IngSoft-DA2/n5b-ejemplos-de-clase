import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Actor } from '../../models/actor.model';
import { ActorService } from '../../services/actor.service';

@Component({
  selector: 'app-actor-list',
  imports: [CommonModule],
  templateUrl: './actor-list.component.html',
  styleUrl: './actor-list.component.css'
})
export class ActorListComponent implements OnInit {
  actors: Actor[] = [];
  isLoading: boolean = true;
  error: string | null = null;

  constructor(private actorService: ActorService) { }

  ngOnInit(): void {
    this.loadActors();
  }

  loadActors(): void {
    this.isLoading = true;
    this.error = null;

    this.actorService.getActors().subscribe({
      next: (actors) => {
        this.actors = actors;
        this.isLoading = false;
      },
      error: () => {
        this.error = 'No se pudieron cargar los actores. Intentalo nuevamente.';
        this.isLoading = false;
      }
    });
  }
}
