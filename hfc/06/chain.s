	.file	"chain.c"
	.def	__main;	.scl	2;	.type	32;	.endef
	.section .rdata,"dr"
.LC0:
	.ascii "taiwan\0"
.LC1:
	.ascii "09:00\0"
.LC2:
	.ascii "17:00\0"
.LC3:
	.ascii "hainan\0"
.LC4:
	.ascii "10:00\0"
.LC5:
	.ascii "18:00\0"
.LC6:
	.ascii "shangchuan\0"
.LC7:
	.ascii "08:00\0"
.LC8:
	.ascii "xiachuan\0"
	.align 8
.LC9:
	.ascii "%s opens at %s, closes at %s.\12\0"
	.text
	.globl	main
	.def	main;	.scl	2;	.type	32;	.endef
	.seh_proc	main
main:
	pushq	%rbp
	.seh_pushreg	%rbp
	movq	%rsp, %rbp
	.seh_setframe	%rbp, 0
	subq	$176, %rsp
	.seh_stackalloc	176
	.seh_endprologue
	call	__main
	leaq	.LC0(%rip), %rax
	movq	%rax, -48(%rbp)
	leaq	.LC1(%rip), %rax
	movq	%rax, -40(%rbp)
	leaq	.LC2(%rip), %rax
	movq	%rax, -32(%rbp)
	movq	$0, -24(%rbp)
	leaq	.LC3(%rip), %rax
	movq	%rax, -80(%rbp)
	leaq	.LC4(%rip), %rax
	movq	%rax, -72(%rbp)
	leaq	.LC5(%rip), %rax
	movq	%rax, -64(%rbp)
	movq	$0, -56(%rbp)
	leaq	.LC6(%rip), %rax
	movq	%rax, -112(%rbp)
	leaq	.LC7(%rip), %rax
	movq	%rax, -104(%rbp)
	leaq	.LC5(%rip), %rax
	movq	%rax, -96(%rbp)
	movq	$0, -88(%rbp)
	leaq	.LC8(%rip), %rax
	movq	%rax, -144(%rbp)
	leaq	.LC7(%rip), %rax
	movq	%rax, -136(%rbp)
	leaq	.LC5(%rip), %rax
	movq	%rax, -128(%rbp)
	movq	$0, -120(%rbp)
	leaq	-80(%rbp), %rax
	movq	%rax, -24(%rbp)
	leaq	-112(%rbp), %rax
	movq	%rax, -56(%rbp)
	leaq	-48(%rbp), %rax
	movq	%rax, -8(%rbp)
	jmp	.L2
.L3:
	movq	-8(%rbp), %rax
	movq	16(%rax), %rcx
	movq	-8(%rbp), %rax
	movq	8(%rax), %rdx
	movq	-8(%rbp), %rax
	movq	(%rax), %rax
	movq	%rcx, %r9
	movq	%rdx, %r8
	movq	%rax, %rdx
	leaq	.LC9(%rip), %rcx
	call	printf
	movq	-8(%rbp), %rax
	movq	24(%rax), %rax
	movq	%rax, -8(%rbp)
.L2:
	cmpq	$0, -8(%rbp)
	jne	.L3
	leaq	-144(%rbp), %rax
	movq	%rax, -24(%rbp)
	leaq	-112(%rbp), %rax
	movq	%rax, -120(%rbp)
	leaq	-48(%rbp), %rax
	movq	%rax, -8(%rbp)
	jmp	.L4
.L5:
	movq	-8(%rbp), %rax
	movq	16(%rax), %rcx
	movq	-8(%rbp), %rax
	movq	8(%rax), %rdx
	movq	-8(%rbp), %rax
	movq	(%rax), %rax
	movq	%rcx, %r9
	movq	%rdx, %r8
	movq	%rax, %rdx
	leaq	.LC9(%rip), %rcx
	call	printf
	movq	-8(%rbp), %rax
	movq	24(%rax), %rax
	movq	%rax, -8(%rbp)
.L4:
	cmpq	$0, -8(%rbp)
	jne	.L5
	movl	$0, %eax
	addq	$176, %rsp
	popq	%rbp
	ret
	.seh_endproc
	.ident	"GCC: (GNU) 4.8.3"
	.def	printf;	.scl	2;	.type	32;	.endef
