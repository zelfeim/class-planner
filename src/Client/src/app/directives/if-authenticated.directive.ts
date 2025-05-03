import { Directive, effect, inject, Input, TemplateRef, ViewContainerRef } from "@angular/core";
import { AuthService } from "../services/auth.service";

@Directive({
    selector: "[cpIfAuthenticated]",
    standalone: true
})
export class IfAuthenticatedDirective {
    @Input()
    public set cpIfAuthenticated(value: boolean) {
        this._shouldDisplay = value;
    }
    private _shouldDisplay: boolean = true;

    private readonly _templateRef: TemplateRef<unknown> = inject(TemplateRef);
    private readonly _viewContainerRef: ViewContainerRef = inject(ViewContainerRef);
    private readonly _authService: AuthService = inject(AuthService);

    constructor() {
        // TODO: Consider if effect is needed at all - will login/logout always trigger page refresh/redirect?
        effect((): void => {
            this._viewContainerRef.clear();

            if (this._authService.isAuthenticated() !== this._shouldDisplay) {
                return;
            }

            this._viewContainerRef.createEmbeddedView(this._templateRef);
        });
    }
}
