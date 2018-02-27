import { Component } from '@angular/core';

@Component({
	selector: 'projectsTable',
	styleUrls: ['./projectsTable.component.css'],
	template: `
		<div class="container">
			<!-- Table Header -->
			<div class="tableHeader">
				<!-- xs, sm view -->
				<div class="row visible-xs visible-sm bg-warning">
					<div class="col-xs-12">
						<div class="row">
							<button>Archive</button>
							<button>This week</button>
						</div>

					</div>
					<div class="col-xs-12 text-center">
						<h1>{{ title }}</h1>
					</div>
				</div>

				<!--  md, lg, xl views -->
				<div class="row hidden-xs hidden-sm">
					<div class="col-md-4">
						<button>Archive</button>
						<button>This week</button>
					</div>
					<div class="col-md-4 text-center">
						<h1>{{ title }}</h1>
					</div>
					<div class="col-md-4">
						<h4 class="pull-right">Week No.</h4>
					</div>
				</div>
			</div>


			<!-- Table Body -->
			<table class="table table-hover table-striped">
				<thead>
					<tr>
						<th>Project #</th>
						<th>WP Count</th>
						<th>Employee Count</th>
						<th>Managers</th>
						<th>Progress</th>
					</tr>
				</thead>
				<tbody>
					<tr>
						<td>P1</td>
						<td>P1023</td>
						<td>7</td>
						<td>Man1, Man2, Man3</td>
						<td>PROGRESS BAR HERE</td>
					</tr>
					<tr>
						<td>P2</td>
						<td>P2004</td>
						<td>12</td>
						<td>Man1, Man4, Man5, Man7</td>
						<td>PROGRESS BAR HERE</td>
					</tr>
				</tbody>
		
			</table>

			<!-- Table Footer -->
			<!-- xs, sm view -->
			<div class="row visible-xs visible-sm">
				<div class="row">
					<button>Stats</button>
				</div>
				<div class="row">
					<button>Manage</button>
				</div>
			</div>

			<!-- md, lg, xl view -->

			<div class="row hidden-xs hidden-sm">
				<div class="pull-right">
					<button>Stas</button>
					<button>Manage</button>
				</div>
			</div>

		</div>

		`
})
export class ProjectsTableComponent {
	title = 'Projects';
	myHero = 'Me';
}
