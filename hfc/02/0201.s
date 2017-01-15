	.file	"0201.c"
	.def	__main;	.scl	2;	.type	32;	.endef
	.section .rdata,"dr"
.LC0:
	.ascii "%d\0"
	.text
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
	.seh_proc	main
main:
	pushq	%rbp
	.seh_pushreg	%rbp
	movq	%rsp, %rbp
	.seh_setframe	%rbp, 0
	subq	$48, %rsp
	.seh_stackalloc	48
	.seh_endprologue
	call	__main
	movl	$1000, -4(%rbp)
	leaq	-4(%rbp), %rax
	movq	%rax, %rcx
	call	change
	movl	-4(%rbp), %eax
	movl	%eax, %edx
	leaq	.LC0(%rip), %rcx
	call	printf
	movl	$0, %eax
	addq	$48, %rsp
	popq	%rbp
	ret
	.seh_endproc
	.globl	change
	.def	change;	.scl	2;	.type	32;	.endef
	.seh_proc	change
change:
	pushq	%rbp
	.seh_pushreg	%rbp
	movq	%rsp, %rbp
	.seh_setframe	%rbp, 0
	.seh_endprologue
	movq	%rcx, 16(%rbp)
	movq	16(%rbp), %rax
	movl	$2000, (%rax)
	movq	16(%rbp), %rax
	movl	(%rax), %eax
	popq	%rbp
	ret
	.seh_endproc
	.ident	"GCC: (GNU) 4.9.3"
	.def	printf;	.scl	2;	.type	32;	.endef
