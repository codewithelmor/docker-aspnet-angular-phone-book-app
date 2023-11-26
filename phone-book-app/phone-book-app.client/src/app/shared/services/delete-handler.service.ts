import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class DeleteHandlerService {

  constructor() { }

  handle(observable: Observable<any>, nextCallback: VoidFunction) {
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: "btn btn-success",
        cancelButton: "btn btn-danger"
      },
      buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Yes, delete it!",
      cancelButtonText: "No, cancel!",
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {

        observable
          .subscribe({
            next: nextCallback,
            error: (error) => {
              console.error(error);

              swalWithBootstrapButtons.fire({
                title: "Failed!",
                text: "Deletion process has failed.",
                icon: "error"
              });
            },
            complete: () => {
              console.info('complete');

              swalWithBootstrapButtons.fire({
                title: "Deleted!",
                text: "Deletion process was completed.",
                icon: "success"
              });
            }
          });

      } else if (
        /* Read more about handling dismissals below */
        result.dismiss === Swal.DismissReason.cancel
      ) {
        swalWithBootstrapButtons.fire({
          title: "Cancelled",
          text: "Deletion process was cancelled.",
          icon: "error"
        });
      }
    });
  }

}