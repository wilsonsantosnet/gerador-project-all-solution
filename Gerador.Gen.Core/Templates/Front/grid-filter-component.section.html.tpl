        <th>
          <span class="table-sort">
            <grid-filter [(vm)]="vm" [show]="showFilters"  [pagination]="false" [navigationProp]="'<#ReletedClass#>'" [type]="'<#type#>'" [fieldName]="'<#propertyName#>'" (filter)="onFilter($event)"></grid-filter>
          </span>
        </th>