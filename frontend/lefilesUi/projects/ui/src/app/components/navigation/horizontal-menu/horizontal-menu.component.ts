import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { MenuItemModel } from '../../../models/navigation/menuItemModel';
import { StorageService } from 'projects/corelib/src/lib/services/storage/storage.service';
import { AuthService } from '../../../services/auth.service';

@Component({
    selector: 'app-horizontal-menu',
    templateUrl: './horizontal-menu.component.html',
    styleUrls: ['./horizontal-menu.component.css'],
    standalone: false
})
export class HorizontalMenuComponent implements OnInit {
  currentPath: string = location.pathname;
  menuItems: MenuItemModel[] = [];
  darkTheme: boolean = false;
  constructor(private activatedRoute: ActivatedRoute, private router: Router, private storageService: StorageService, private authService: AuthService) { }

  ngOnInit(): void {
    this.pathListener();
    this.createMenuItems();
  }
  switchTheme(theme: string) {
    var container = document.getElementsByClassName("ui-container")[0];
    if (theme == "dark") {
      container.classList.remove("theme-dark");
      container.classList.remove("theme-light");
      container.classList.add("theme-dark");
    } else if (theme == "light") {
      container.classList.remove("theme-dark");
      container.classList.remove("theme-light");
      container.classList.add("theme-light");
    }
  }
  pathListener() {
    this.router.events.subscribe({
      next: (param) => {
        if (param instanceof NavigationEnd) {
          this.currentPath = param.url;
        }
      }
    })
  }
  createMenuItems() {
    this.menuItems = [
      {
        labelKey: "menuItem.main",
        path: "/main",
        icon: "bi bi-house-door-fill"
      },
      {
        labelKey: "menuItem.files",
        path: "/files",
        icon: "bi bi-folder-fill"
      }
    ]
  }
  logout() {
    this.authService.logout();
  }

}
